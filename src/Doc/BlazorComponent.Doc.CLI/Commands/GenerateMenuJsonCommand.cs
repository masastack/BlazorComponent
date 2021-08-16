using BlazorComponent.Doc.CLI.Comparers;
using BlazorComponent.Doc.CLI.Interfaces;
using BlazorComponent.Doc.CLI.Wrappers;
using BlazorComponent.Doc.Extensions;
using BlazorComponent.Doc.Models;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorComponent.Doc.CLI.Commands
{
    public class GenerateMenuJsonCommand : IAppCommand
    {
        public string Name => "menu2json";

        //private static readonly Dictionary<string, int> _sortMap = new Dictionary<string, int>()
        //{
        //    ["Docs"] = -2,
        //    ["文档"] = -2,
        //    ["Overview"] = -1,
        //    ["组件总览"] = -1,
        //    ["General"] = 0,
        //    ["通用"] = 0,
        //    ["Layout"] = 1,
        //    ["布局"] = 1,
        //    ["Navigation"] = 2,
        //    ["导航"] = 2,
        //    ["Data Entry"] = 3,
        //    ["数据录入"] = 3,
        //    ["Data Display"] = 4,
        //    ["数据展示"] = 4,
        //    ["Feedback"] = 5,
        //    ["反馈"] = 5,
        //    ["Localization"] = 6,
        //    ["Other"] = 7,
        //    ["其他"] = 7,
        //    ["Charts"] = 8,
        //    ["图表"] = 8
        //};

        //private static readonly Dictionary<string, string> _demoCategoryMap = new Dictionary<string, string>()
        //{
        //    ["Components"] = "组件",
        //    ["Charts"] = "图表"
        //};

        public void Execute(CommandLineApplication command)
        {
            command.Description = "Generate json file for menu";
            command.HelpOption();

            var demoDirArgument = command.Argument(
                "demoDir", "[Required] The directory of docs files.");

            var docsDirArgument = command.Argument(
                "docsDir", "[Required] The directory of docs files.");

            var outputArgument = command.Argument(
                "output", "[Required] The directory where the json file to output");

            command.OnExecute(() =>
            {
                string demoDir = demoDirArgument.Value;
                string docsDir = docsDirArgument.Value;
                string output = outputArgument.Value;

                if (string.IsNullOrEmpty(demoDir) || !Directory.Exists(demoDir))
                {
                    Console.WriteLine("Invalid demoDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(docsDir) || !Directory.Exists(docsDir))
                {
                    Console.WriteLine("Invalid docsDir.");
                    return 1;
                }

                if (string.IsNullOrEmpty(output))
                {
                    output = "./";
                }

                string demoDirectory = Path.Combine(Directory.GetCurrentDirectory(), demoDir);
                string docsDirectory = Path.Combine(Directory.GetCurrentDirectory(), docsDir);

                GenerateFiles(demoDirectory, docsDirectory, output);

                return 0;
            });
        }

        private void GenerateFiles(string demoDirectory, string docsDirectory, string output)
        {
            var demoDirectoryInfo = new DirectoryInfo(demoDirectory);
            if (!demoDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", demoDirectory);
                return;
            }

            var docsDirectoryInfo = new DirectoryInfo(docsDirectory);
            if (!docsDirectoryInfo.Exists)
            {
                Console.WriteLine("{0} is not a directory", docsDirectory);
                return;
            }

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var docsMenuList = GetSubMenuList(docsDirectoryInfo, true).ToList();

            var categoryDemoMenuList = new Dictionary<string, Dictionary<string, IEnumerable<DemoMenuItemModel>>>();
            var allComponentMenuList = new List<Dictionary<string, DemoMenuItemModel>>();

            foreach (var subDemoDirectory in demoDirectoryInfo.GetFileSystemInfos().OrderBy(r=>r.Name))
            {
                var category = subDemoDirectory.Name;

                var componentMenuList = GetSubMenuList(subDemoDirectory as DirectoryInfo, false).ToList();

                allComponentMenuList.AddRange(componentMenuList);

                var componentMenuI18N = componentMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                foreach (var component in componentMenuI18N)
                {
                    if (!categoryDemoMenuList.ContainsKey(component.Key))
                    {
                        categoryDemoMenuList[component.Key] = new Dictionary<string, IEnumerable<DemoMenuItemModel>>();
                    }
                    categoryDemoMenuList[component.Key].Add(category, component.Value);
                }
            }

            var docsMenuI18N = docsMenuList
                .SelectMany(x => x)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(x => x.Value));

            foreach (var lang in new[] { "zh-CN", "en-US" })
            {
                var menus = new List<DemoMenuItemModel>();

                var children = docsMenuI18N[lang].OrderBy(x => x.Order).ToArray();

                var categoryComponent = categoryDemoMenuList[lang];

                var componentMenus = new List<DemoMenuItemModel>();
                foreach (var component in categoryComponent)
                {
                    componentMenus.Add(new DemoMenuItemModel()
                    {
                        Order = Array.IndexOf(ConfigWrapper.Config.GenerateRule.Menus.Select(x => x.Key).ToArray(), component.Key) + 1,
                        Title = ConfigWrapper.Config.GenerateRule.Menus.First(menu => menu.Key == component.Key).Descriptions.First(desc => desc.Lang == lang).Description,
                        Type = "component",
                        Url = component.Key.ToLowerInvariant(),
                        Children = component.Value.OrderBy(x => x.Order).ToArray()
                    });
                }

                //Children 4 will be component menu
                children[4].Children = componentMenus[0].Children.SelectMany(r => r.Children)
                    .OrderBy(r => r.Order)
                    .ThenBy(r => r.Title)
                    .ToArray();
                menus.AddRange(children);

                var json = JsonSerializer.Serialize(menus, jsonOptions);

                var configFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), output);
                if (!Directory.Exists(configFileDirectory))
                {
                    Directory.CreateDirectory(configFileDirectory);
                }

                var configFilePath = Path.Combine(configFileDirectory, $"menu.{lang}.json");

                if (File.Exists(configFilePath))
                {
                    File.Delete(configFilePath);
                }

                File.WriteAllText(configFilePath, json);

                var componentI18N = allComponentMenuList
                    .SelectMany(x => x)
                    .GroupBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Select(o => o.Value));

                var demos = componentI18N[lang];

                var demosPath = Path.Combine(configFileDirectory, $"demos.{lang}.json");

                if (File.Exists(demosPath))
                {
                    File.Delete(demosPath);
                }

                json = JsonSerializer.Serialize(demos, jsonOptions);
                File.WriteAllText(demosPath, json);

                var docs = docsMenuI18N[lang];
                var docsPath = Path.Combine(configFileDirectory, $"docs.{lang}.json");

                if (File.Exists(docsPath))
                {
                    File.Delete(docsPath);
                }

                json = JsonSerializer.Serialize(docs, jsonOptions);
                File.WriteAllText(docsPath, json);
            }
        }

        private IEnumerable<Dictionary<string, DemoMenuItemModel>> GetSubMenuList(DirectoryInfo directory, bool isDocs)
        {
            if (isDocs)
            {
                foreach (var menuDir in directory.GetDirectories())
                {
                    foreach (var menuItem in menuDir.GetFileSystemInfos().OrderBy(r=>r.Name))
                    {
                        if (menuItem.Name == "index.json")
                        {
                            var content = File.ReadAllText(menuItem.FullName, Encoding.UTF8);
                            var data = JObject.Parse(content);
                            foreach (var titleItem in data["title"].ToArray())
                            {
                                yield return new Dictionary<string, DemoMenuItemModel>
                                {
                                    [titleItem["lang"].ToString()] = new DemoMenuItemModel
                                    {
                                        Order = data["order"].ToObject<int>(),
                                        Title = titleItem["content"].ToString(),
                                        Url = $"docs/{menuDir.Name}",
                                        Icon = data["icon"].ToString(),
                                        Type = "menuItem",
                                        Children = GetSubMenuChildren(menuDir, titleItem["lang"].ToString()).OrderBy(r => r.Order).ThenBy(r => r.Title).ToArray()
                                    }
                                };
                            }
                        }
                    }
                }
            }
            else
            {
                var componentI18N = GetComponentI18N(directory);
                foreach (var group in componentI18N.GroupBy(x => x.Value.Type))
                {
                    var menu = new Dictionary<string, DemoMenuItemModel>();

                    foreach (var component in group.GroupBy(x => x.Key))
                    {
                        menu.Add(component.Key, new DemoMenuItemModel()
                        {
                            Order = ConfigWrapper.DocsNavOrder[group.Key],
                            Title = group.Key,
                            Type = "itemGroup",
                            Children = group.Select(x => new DemoMenuItemModel()
                            {
                                Title = x.Value.Title,
                                SubTitle = x.Value.SubTitle,
                                Url = $"{directory.Name.ToLowerInvariant()}/{x.Value.Title.ToLower()}",
                                Type = "menuItem",
                                Order = x.Value.Order,
                                Cover = x.Value.Cover
                            })
                            .OrderBy(x => x.Title, new MenuComparer())
                            .ToArray(),
                        });
                    }

                    yield return menu;
                }
            }
        }

        private IEnumerable<DemoMenuItemModel> GetSubMenuChildren(DirectoryInfo menuDir, string lang)
        {
            foreach (var menuItem in menuDir.GetFileSystemInfos().OrderBy(r=>r.Name))
            {
                if (menuItem.Extension == ".md")
                {
                    var args = menuItem.Name.Split('.');
                    if (args[1] == lang)
                    {
                        var content = File.ReadAllText(menuItem.FullName);
                        var data = DocWrapper.ParseHeader(content);
                        var titles = DocWrapper.ParseTitle(content);

                        yield return new DemoMenuItemModel
                        {
                            Order = Convert.ToInt32(data["order"]),
                            Title = data["title"],
                            Url = $"docs/{menuDir.Name}/{args[0]}",
                            Type = "menuItem",
                            Contents = titles.Select(r => new ContentsItem
                            {
                                Href = $"docs/{menuDir.Name}/{args[0]}/#section-" + HashHelper.Hash(r),
                                Title = r
                            }).ToList()
                        };
                    }
                }
            }
        }

        private IEnumerable<KeyValuePair<string, DemoComponentModel>> GetComponentI18N(DirectoryInfo directory)
        {
            IList<Dictionary<string, DemoComponentModel>> componentList = null;

            foreach (var component in directory.GetFileSystemInfos().OrderBy(r=>r.Name))
            {
                if (!(component is DirectoryInfo componentDirectory))
                    continue;

                var componentDic = new Dictionary<string, DemoComponentModel>();

                var docDir = componentDirectory.GetFileSystemInfos("doc")[0];

                foreach (FileSystemInfo docItem in (docDir as DirectoryInfo).GetFileSystemInfos().OrderBy(r=>r.Name))
                {
                    string language = docItem.Name.Replace("index.", "").Replace(docItem.Extension, "");
                    string content = File.ReadAllText(docItem.FullName);
                    (Dictionary<string, string> Meta, string Desc, string ApiDoc) docData = DocWrapper.ParseDemoDoc(content);

                    componentDic.Add(language, new DemoComponentModel()
                    {
                        Title = docData.Meta["title"],
                        SubTitle = docData.Meta.TryGetValue("subtitle", out var subtitle) ? subtitle : null,
                        Type = docData.Meta["type"],
                        Order = docData.Meta.TryGetValue("order", out var order) ? int.Parse(order) : 0,
                        Desc = docData.Desc,
                        ApiDoc = docData.ApiDoc,
                        Cover = docData.Meta.TryGetValue("cover", out var cover) ? cover : null,
                    }); ;
                }

                componentList ??= new List<Dictionary<string, DemoComponentModel>>();
                componentList.Add(componentDic);
            }

            if (componentList == null)
                return Enumerable.Empty<KeyValuePair<string, DemoComponentModel>>();

            var componentI18N = componentList
                .SelectMany(x => x).OrderBy(x => ConfigWrapper.DocsNavOrder[x.Value.Type]);

            return componentI18N;
        }
    }
}
