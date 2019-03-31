import Vue from 'vue'
import Vuex from 'vuex'
import jwt_decode from 'jwt-decode';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        jwtToket: localStorage.getItem('jwtToken') || '',
        actions: {
            admin: [{
                name: 'events',
                title: 'События'
            },
            {
                name: 'tasks',
                title: 'Задачи'
            },
            {
                name: 'partners',
                title: 'Партнеры'
            },
            {
                name: 'users',
                title: 'Пользователи'
            }],
            organizer: [{
                name: 'events',
                title: 'События'
            },
            {
                name: 'tasks',
                title: 'Задачи'
            }],
            volunteer: [{
                name: 'tasks',
                title: 'Задачи'
            },
            {
                name: 'partners',
                title: 'Партнеры'
            }]
        },
        activeAdminPage: null,
        roles: {
            'admin': 'admin',
            'organizer': 'organizer',
            'volunteer': 'volunteer'
        }
    },
    mutations: {
        setJwtToken (state, token) {
            state.jwtToken = token;
            localStorage.setItem('jwtToken', token);
        },
        clearJwtToken (state) {
            state.jwtToken = '';
            localStorage.removeItem('jwtToken');
        },
        setActiveAdminPage (state, page) {
            state.activeAdminPage = page;
        }
    },
    getters: {
        activeAdminPage (state) {
            return state.activeAdminPage;
        },
        isAuthenticated (state) {
            return !!state.jwtToken;
        },
        jwtToken (state) {
            return state.jwtToken;
        },
        role (state) {
            return jwt_decode('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJyb2xlIjoiYWRtaW4ifQ.RAsF_THrV3ujtaBIaauKlTltnPYjowkp6jQ-dCls1GI').role;
        },
        roles (state) {
            return state.roles;
        },
        actions (state, getters) {
            return state.actions[getters.role]
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
                        context.commit('setJwtToken', token.access_token);
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
