import Vue from 'vue'
import Resource from 'vue-resource';
import BootstrapVue from 'bootstrap-vue'
import Datepicker from 'vuejs-datepicker';
import { polyfill } from 'es6-object-assign'

polyfill();

import { library } from '@fortawesome/fontawesome-svg-core'
import { faPhone, faPaperPlane, faPen, faTrash } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import App from './App.vue'
import router from './router'
import store from './store'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

library.add(faPhone, faPaperPlane, faPen, faTrash);
Vue.component('font-awesome-icon', FontAwesomeIcon);
Vue.component('date-picker', Datepicker);
Vue.use(BootstrapVue);
Vue.use(Resource);

Vue.config.productionTip = false;

Vue.http.interceptors.push((request, next) => {
    request.headers.set('Authorization', 'Bearer ' + store.getters.jwtToken);
    next();
});

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
