<template>
    <div class="row">
        <div class="title-row">
            <h1>Пользователи</h1>
            <b-button variant="success" @click="isModelNewUser = true">Создать нового пользователь</b-button>
        </div>
        <div class="table-responsive">
            <table class="table">
                <thead>
                <tr>
                    <th>#</th>
                    <th>Имя</th>
                    <th>Фамилия</th>
                    <th>Отчество</th>
                    <th>Телефон</th>
                    <th>Редактирование</th>
                </tr>
                </thead>
                <tbody v-if="users">
                <tr v-for="user in users.data" :key="user.id">
                    <td>{{user.id}}</td>
                    <td>{{user.name}}</td>
                    <td>{{user.surname}}</td>
                    <td>{{user.patronymic}}</td>
                    <td>{{user.phone}}</td>
                    <td><font-awesome-icon @click="updateUsers(user)" icon="pen" color="#000"style="margin-right: 50px"/><font-awesome-icon icon="trash" @click="deleteUsers(user)" color="#000"/></td>
                </tr>
                </tbody>
                <div v-else class="spinner--block">
                    <b-spinner style="width: 4rem; height: 4rem;" label="Large Spinner"></b-spinner>
                </div>
            </table>
        </div>
        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item" @click="decPage">
                    <span class="page-link">&laquo;</span>
                </li>
                <li class="page-item"><div class="page-link">{{activePage}} / {{countPage}}</div></li>
                <li class="page-item" @click="incPage">
                    <span class="page-link">&raquo;</span>
                </li>
            </ul>
        </nav>
        <div v-bind:class="`modal ${isModelNewUser ? 'show' : 'fade'}`">
            <modal-create view="users"></modal-create>
        </div>
        <div v-bind:class="`modal ${isModalUpUser ? 'show' : 'fade'}`">
            <modal-update view="users" :pattern="updateU"></modal-update>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from "vuex";

    export default {
        components: {
            ModalCreate: () => import('./ModalCreate.vue'),
            ModalUpdate: () => import('./ModalUpdate.vue')
        },
        data() {
            return {
                users: null,
                limit: 50,
                offset: 0,
                countPage: 0,
                activePage: 1,
                isModelNewUser: false,
                isModalUpUser: false,
                updateU: null
            }
        },
        methods: {
            updateUsers: function (user) {
                this.updateU = user;
                this.isModalUpUser = true;
            },
            deleteUsers: function (user) {
                this.$http.delete(`http://172.20.0.3/api/users/${user.id}`).then(res => {
                    this.users.data.splice(this.users.data.indexOf(user), 1)
                })
            },
            incPage: function() {
                if (this.activePage < this.countPage) {
                    this.activePage += 1;
                    this.offset += this.limit;
                    this.getUsers()
                }
            },
            decPage: function() {
                if (this.activePage !== 1 && this.activePage > 1) {
                    this.activePage -= 1;
                    this.offset -= this.limit;
                    this.getUsers()
                }
            },
            getUsers: function () {
                this.$http.get(`http://172.20.0.3/api/users/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                    this.users = res.body;
                    this.countPage = Math.floor(this.users['totalCount'] / this.limit + ( this.users['totalCount'] % this.limit !== 0 ? 1 : 0));
                }, e => {
                    alert('При получении даных произошла ошибка');
                })
            }
        },
        beforeMount() {
            this.getUsers()
        },
        mounted() {
            this.$on('fadeModal', (is) => {
                this.isModelNewUser = is;
                this.isModalUpUser = is;
                this.getUsers()
            })
        },
        computed: {
            ...mapGetters(['role'])
        }
    }
</script>

<style scoped>

</style>