<template>
    <div class="main">
        <div class="container-fluid" v-if="isAuthenticated">
            <div v-bind:id="role" v-if="role === roles.admin">
                <admin-header></admin-header>
                <events v-if="activeAdminPage === 'events'"></events>
                <partners v-else-if="activeAdminPage === 'partners'"></partners>
                <tasks v-else-if="activeAdminPage === 'tasks'"></tasks>
                <users v-else-if="activeAdminPage === 'users'"></users>
                <interactions v-else-if="activeAdminPage === 'interactions'"></interactions>
                <events v-else></events>
            </div>
            <div v-bind:id="role" v-else-if="role === roles.organizer">
                <admin-header></admin-header>
                <events v-if="activeAdminPage === 'events'"></events>
                <tasks v-else-if="activeAdminPage === 'tasks'"></tasks>
            </div>
            <div v-bind:id="role" v-else-if="role === roles.volunteer">
                <admin-header></admin-header>
                <tasks v-if="activeAdminPage === 'tasks'"></tasks>
                <partners v-else-if="activeAdminPage === 'partners'"></partners>
            </div>
        </div>
        <div class="container-fluid row justify-content-center align-items-center auth" v-else>
            <div class="auth-control">
                <div class="row">
                    <div class="content">
                        <h1>Выполните вход</h1>
                        <div class="form-group">
                            <label for="auth-login">Username</label>
                            <input type="text" id="auth-login" placeholder="Username" class="form-control" v-model="username">
                        </div>
                        <div class="form-group">
                            <label for="auth-pass">Password</label>
                            <input type="password" id="auth-pass" placeholder="Password" class="form-control" v-model="password">
                        </div>
                        <button type="submit" class="btn btn-primary" @click="login">Войти</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from "vuex";
    
    export default {
        data() {
            return {
                username: '',
                password: ''
            }
        },
        components: {
            Events: () => import('../components/Events.vue'),
            Partners: () => import('../components/Partners.vue'),
            Tasks: () => import('../components/Tasks.vue'),
            Users: () => import('../components/Users.vue'),
            Interactions: () => import('../components/Interactions.vue'),
            AdminHeader: () => import('../components/AdminHeader.vue')
        },
        methods:{
            login: function() {
                this.$store.dispatch('auth', {
                    username: this.username,
                    password: this.password
                }).then(token => {
                    alert('Вход выполнен успешно!');
                }).catch(e => {
                    alert('Ошибка входа. Повторите попытку снова.');
                    this.clearForm();
                });
            },
            clearForm () {
                this.username = '';
                this.password = '';
            }, 
        },
        computed: {
            ...mapGetters(['isAuthenticated', 'role', 'roles', 'activeAdminPage'])
        }
    }
</script>
<style lang="scss" scoped>
    .main {
        background-color: rgba(255, 202, 191, 0.09);
        height: 100vh;
        .auth {
            height: 100%;
        }
    }
</style>
