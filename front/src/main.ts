import { createApp } from 'vue';
import App from './App.vue';
import { createPinia } from 'pinia';
import './style.css'
import PrimeVue from 'primevue/config';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import router from './router';
import { setApiUrl } from './model/api';

let app = createApp(App);
const getRuntimeConf = async () => {
  const runtimeConf = await fetch('/config/runtime-config.json');
  return await runtimeConf.json();
};

getRuntimeConf().then((json) => {
    setApiUrl(json.API_URL);
  
    app.use(createPinia());
    app.use(PrimeVue);
    app.use(router);
  
    app.component('DataTable', DataTable);
    app.component('Column', Column);
  
    app.mount('#app');
  });
