<template>
  <AddEventForm :editId="eventId" />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useRoute } from 'vue-router';
import AddEventForm from '@/components/event/AddEventForm.vue';


export default defineComponent({
  setup() {
    const params = useRoute().params;
    const eventId = +params.id;
    return {eventId};
  },
  components: {AddEventForm}
});


</script>
