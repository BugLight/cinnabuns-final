import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        jwtToket: localStorage.getItem('jwtToken') || ''
    },
    mutations: {
        setJwtToken (state, token) {
            state.jwtsToken = token;
            localStorage.setItem('jwtToken', token);
        },
        clearJwtToken (state) {
            state.jwtToken = '';
            localStorage.removeItem('jwtToken');
        }
    },
    getters: {
        isAuthenticated (state) {
            return !!state.jwtToken;
        },
        jwtToken (state) {
            return state.jwtToken;
        }
    },
    actions: {
        auth (context, login) {
            return new Promise((res, rej) => {
                Vue.http.post('/api/auth', login).then(response => {
                    if (response.status != 200) {
                        context.commit('clearJwtToken');
                        rej();
                    } else {
                        let token = response.body;
                        context.commit('setJwtToken', token);
                        res(token);
                    }
                }).catch(e => {
                    context.commit('clearJwtToken');
                    rej(e);
                });
            });
        }
    }
})
