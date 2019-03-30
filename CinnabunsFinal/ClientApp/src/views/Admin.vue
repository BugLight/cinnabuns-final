<template>
    <div class="main">
        <div class="container-fluid" v-if="isAuthenticated">
            
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
            ...mapGetters(['isAuthenticated'])
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
