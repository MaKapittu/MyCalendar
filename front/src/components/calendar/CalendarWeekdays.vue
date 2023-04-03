<template>
    <ol class="day-of-week">
      <li v-for="weekday in weekdays"
        :key="weekday"> 
        {{ weekday }}
      </li>
    </ol>
  </template>
  
  <script>

  
  export default {
    name: 'CalendarWeekdays',
    data: () => ({
      weekdays: ["Mon", "Tue", "Wen", "Thu", "Fri", "Sat", "Sun"]
    }),
  }
  </script>

<style scoped>
.day-of-week {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
}

.day-of-week > * {
  text-align: right;
  padding-right: 5px;
}
</style>