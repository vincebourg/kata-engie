// css
import '@/assets/main.css';
import 'mapbox-gl/dist/mapbox-gl.css';

import { createApp } from 'vue';
import App from '@/App.vue';
import router from '@/router';

const app = createApp(App);
app.use(router);
app.mount('#app');
