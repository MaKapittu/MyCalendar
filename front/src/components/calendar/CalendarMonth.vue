<template>
  <br>
  <div>
  </div>
  <br>
  <div class="calendar-month">

    <div class="calendar-month-header">
      <CalendarDateIndicator
        :selected-date="selectedDate"
        class="calendar-month-header-selected-month"
      />
        
      <CalendarDateSelector
        :current-date="today"
        :selected-date="selectedDate"
        @dateSelected="selectDate"
      />
    </div>

    <CalendarWeekdays />

    <ol class="days-grid">
      <CalendarMonthDayItem
        v-for="day in days"
        :key="day.date"
        :day="day"
        :is-today="day.date === today"
        @click="() => goDayPage(day.date)"
      />
    </ol>
  </div>
</template>

<script>
import dayjs from "dayjs";
  import CalendarDateIndicator from "./CalendarDateIndicator.vue";
  import CalendarDateSelector from "./CalendarDateSelector.vue";
  import CalendarMonthDayItem from "./CalendarMonthDayItem.vue";
  import CalendarWeekdays from "./CalendarWeekdays.vue";
  import weekday from "dayjs/plugin/weekday";
  import weekOfYear from "dayjs/plugin/weekOfYear";
import { AppPaths } from "@/router";

  dayjs.extend(weekday);
  dayjs.extend(weekOfYear);
  
  export default {
  name: "CalendarMonth",

  components: {
    CalendarDateIndicator,
    CalendarDateSelector,
    CalendarMonthDayItem,
    CalendarWeekdays
  },

  data() {
    return {
      selectedDate: dayjs(),
    };
  },

  computed: {
    days() {
      return [
        ...this.previousMonthDays,
        ...this.currentMonthDays,
        ...this.nextMonthDays,
      ];
    },

    today() {
      return dayjs().format("YYYY-MM-DD");
    },

    month() {
      return Number(this.selectedDate.format("M"));
    },

    year() {
      return Number(this.selectedDate.format("YYYY"));
    },

    numberOfDaysInMonth() {
      return dayjs(this.selectedDate).daysInMonth();
    },

    currentMonthDays() {
      return [...Array(this.numberOfDaysInMonth)].map((day, index) => {
        return {
          date: dayjs(`${this.year}-${this.month}-${index + 1}`).format(
            "YYYY-MM-DD"
          ),
          isCurrentMonth: true,
        };
      });
    },

    previousMonthDays() {
      const firstDayOfTheMonthWeekday = this.getWeekday(
        this.currentMonthDays[0].date
      );
      const previousMonth = dayjs(`${this.year}-${this.month}-01`).subtract(
        1,
        "month"
      );

      // Cover first day of the month being sunday (firstDayOfTheMonthWeekday === 0)
      const visibleNumberOfDaysFromPreviousMonth = firstDayOfTheMonthWeekday
        ? firstDayOfTheMonthWeekday - 1
        : 6;

      const previousMonthLastMondayDayOfMonth = dayjs(
        this.currentMonthDays[0].date
      )
        .subtract(visibleNumberOfDaysFromPreviousMonth, "day")
        .date();

      return [...Array(visibleNumberOfDaysFromPreviousMonth)].map(
        (day, index) => {
          return {
            date: dayjs(
              `${previousMonth.year()}-${previousMonth.month() + 1}-${
                previousMonthLastMondayDayOfMonth + index
              }`
            ).format("YYYY-MM-DD"),
            isCurrentMonth: false,
          };
        }
      );
    },

    nextMonthDays() {
      const lastDayOfTheMonthWeekday = this.getWeekday(
        `${this.year}-${this.month}-${this.currentMonthDays.length}`
      );

      const nextMonth = dayjs(`${this.year}-${this.month}-01`).add(1, "month");

      const visibleNumberOfDaysFromNextMonth = lastDayOfTheMonthWeekday
        ? 7 - lastDayOfTheMonthWeekday
        : lastDayOfTheMonthWeekday;

      return [...Array(visibleNumberOfDaysFromNextMonth)].map((day, index) => {
        return {
          date: dayjs(
            `${nextMonth.year()}-${nextMonth.month() + 1}-${index + 1}`
          ).format("YYYY-MM-DD"),
          isCurrentMonth: false,
        };
      });
    },
  },

  methods: {
    getWeekday(date) {
      return dayjs(date).weekday();
    },
    goDayPage(date) {
      this.$router.push(AppPaths.calendarday + "/" + date);
    },

    selectDate(newSelectedDate) {
      this.selectedDate = newSelectedDate;
    },
  },
};
</script>

<style scoped>

ol,
li {
  padding: 0;
  margin: 0;
  list-style: none;
}
.calendar-month {
  --grey-50: #dbc9b8;
  --grey-100: #a17e61;
  --grey-200: #855832;
  --grey-300: #74482a;
  --grey-800: #362925;
  --grid-gap: 1px;
  --day-label-size: 22px;
  position: relative;
  background-color:var(--grey-200);
  border: solid 1px var(--grey-800);
  width: 60%;
  margin-left: 20%;
  margin-right: 50%;
  margin-top: -32px;
  box-shadow: 0 0 15px 2px rgb(169, 169, 169);  
}
.day-of-week {
  color: var(--grey-800);
  font-size: 18px;
  background-color: #fff;
  padding-bottom: 5px;
  padding-top: 10px;
}

.day-of-week,
.days-grid {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  height: 100%;
  position: relative;
  grid-column-gap: var(--grid-gap);
  grid-row-gap: var(--grid-gap);
  border-top: solid 1px var(--grey-800);
}

.calendar-month-header {
  display: flex;
  justify-content: space-between;
  background-color: #fff;
  padding: 10px;
  color: var(--grey-800);
}
</style>
