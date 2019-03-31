import Vue from 'vue'
import Vuex from 'vuex'
import jwt_decode from 'jwt-decode';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        jwtToken: localStorage.getItem('jwtToken') || '',
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
            },
            {
                name: 'interactions',
                title: 'Взаимодействие'
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
            return state.jwtToken ? jwt_decode(state.jwtToken).role : '';
        },
        uid (state) {
            return state.jwtToken ? jwt_decode(state.jwtToken).sub : '';
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
