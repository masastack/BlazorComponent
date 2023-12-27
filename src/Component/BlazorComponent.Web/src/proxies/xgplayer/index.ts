import Xgplayer, { IPlayerOptions } from "xgplayer";
import MusicPreset, { Music } from "xgplayer-music";
import Mobile from "xgplayer/es/plugins/mobile";
import Play from "xgplayer/es/plugins/play";
import Playbackrate from "xgplayer/es/plugins/playbackRate";
import Progress from "xgplayer/es/plugins/progress";
import Time from "xgplayer/es/plugins/time";
import Volume from "xgplayer/es/plugins/volume";

export type XgplayerOptions = Omit<
  IPlayerOptions,
  "id" | "el" | "url" | "plugins"
> & {
  music?: Music;
};

class XgplayerProxy {
  el: HTMLElement;
  initOptions: XgplayerOptions;
  player: Xgplayer;

  constructor(
    selector: string,
    url: IPlayerOptions["url"],
    options: XgplayerOptions
  ) {
    const el: HTMLElement = document.querySelector(selector);

    if (!el) {
      throw new Error(
        "[Xgplayer] this selector of DOM node that player to mount on is required."
      );
    }

    if (!url) {
      throw new Error("[Xgplayer] this media resource url is required.");
    }

    this.initOptions = options;
    this.el = el;
    this.init(url, options);
  }

  invokeVoid(prop: string, args: any[]) {
    if (this.player[prop] && typeof this.player[prop] === "function") {
      this.player[prop](...args);
    }
  }

  getPropsAndStates() {
    return {
      autoplay: this.player.autoplay,
      crossOrigin: this.player.crossOrigin,
      currentSrc: this.player.currentSrc,
      currentTime: this.player.currentTime,
      duration: this.player.duration,
      cumulateTime: this.player.cumulateTime,
      volume: this.player.volume,
      muted: this.player.muted,
      defaultMuted: this.player.defaultMuted,
      playbackRate: this.player.playbackRate,
      loop: this.player.loop,
      src: this.player.src,
      lang: this.player.lang,
      version: this.player.version,
      state: this.player.state,
      ended: this.player.ended,
      paused: this.player.paused,
      networkState: this.player.networkState,
      readyState: this.player.readyState,
      isFullscreen: this.player.isFullscreen,
      isCssFullscreen: this.player.isCssfullScreen,
      isSeeking: this.player.isSeeking,
      isActive: this.player.isActive,
    };
  }

  init(url: IPlayerOptions["url"], options: XgplayerOptions) {
    let playerOptions: IPlayerOptions = {
      el: this.el,
      url
    };

    if (options.music) {
      playerOptions = {
        ...playerOptions,
        presets: [MusicPreset],
        plugins: [Mobile, Progress, Play, Playbackrate, Time, Volume],
        ...options,
      };
    } else {
      playerOptions = {
        ...playerOptions,
        ...options,
      };
    }

    if (window.MasaBlazor.xgplayerPlugins) {
      playerOptions.plugins = [
        ...playerOptions.plugins ?? [],
        ...window.MasaBlazor.xgplayerPlugins,
      ];

      playerOptions = {
        ...playerOptions,
        ...window.MasaBlazor.xgplayerPluginOptions,
      };
    }

    this.player = new Xgplayer(playerOptions);
  }

  destroy() {
    this.player.destroy();
    this.player = null;
  }
}

export function init(
  selector: string,
  url: IPlayerOptions["url"],
  options: XgplayerOptions
) {
  const instance = new XgplayerProxy(selector, url, options);
  return instance;
}
