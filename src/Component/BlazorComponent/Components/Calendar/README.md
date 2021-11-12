## TODO
1. 缺少**ref**及**ref使用的方法**， 例如： this.$refs.calendar.checkChange()等
2. 缺少全部事件及调用方法
3. 除Demo:Week.razor中daily部分场景外**其他均未测试**（原因如上一条）
4. **styler()**方法object类型动态拼接style
5. **CreateNativeLocaleFormatter()**方法时间转换无法使用typescript原生方法，部分未知场景未实现

***
## 组件间继承关系
1. BCalendar -> **BCalendarEvents** -> **BCalendarBase**
2. BCalendarCategory -> BCalendarDaily
3. BCalendarDaily -> **BCalendarIntervals** -> **BCalendarBase**
4. BCalendarMonthly -> BCalendarWeekly
5. BCalendarWeekly -> **BCalendarBase**

## 缺失的文件或方法

```
mixins/events.ts: updateEventVisibility,getEventsMap,getVisibleEvents
```
```
mixins/intervals.ts:getTimestampAtEvent,scrollToTime
```
```
mixins/mouse.ts
```
```
mixins/time.ts
```
```
util/timestamp.ts: nextMinutes
```
