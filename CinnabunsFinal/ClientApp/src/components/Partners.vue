<template>
    <div class="row">
        <div class="title-row">
            <h1>Партнеры</h1>
            <b-button variant="success">Создать нового партнера</b-button>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tr>
                    <th>#</th>
                    <th>Название / Имя</th>
                    <th>ИНН</th>
                    <th>Web-site</th>
                    <th>Фамилия</th>
                    <th>Отчество</th>
                    <th>Телефон</th>
                    <th>Email</th>
                </tr>
                <div v-if="partners">
                    <tr v-for="partner in partners" :key="partner.id">
                        <td>{{partner.name}}</td>
                        <td><font-awesome-icon icon="pen" color="#000" @click="deletePartner(partner)"/> <font-awesome-icon icon="trash" color="#000"/></td>
                    </tr>
                </div>
                <div v-else class="spinner--block">
                    <b-spinner style="width: 4rem; height: 4rem;" label="Large Spinner"></b-spinner>
                </div>
            </table>
        </div>
        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item">
                    <span class="page-link" aria-hidden="true">&laquo;</span>
                </li>
                <li v-for="(page, i) in countPage" :key="page.id" v-bind:class="`page-item ${activePage === i+1 ? 'active' : ''}`"><div class="page-link" @click="selectPage(i)">{{++i}}</div></li>
                <li class="page-item">
                    <span class="page-link" aria-hidden="true">&raquo;</span>
                </li>
            </ul>
        </nav>
    </div>
</template>

<script>
    import { mapGetters } from "vuex";

    export default {
        data() {
            return {
                partners: null,
                limit: 50,
                offset: 0,
                countPage: 10,
                activePage: 1
            }
        },
        methods: {
            deletePartner: function (partner) {
                this.$http.delete(`/api/partners/?${partner.id}`).then(res => {
                    this.partners.splice(this.partners.indexOf(partner), 1)
                })
            },
            selectPage: function (i) {
                this.activePage = i;
                this.getEvents()
            },
            getEvents: function () {
                this.$http.get(`/api/partners/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                    this.partners = res.body;
                    this.offset = this.offset + this.limit;
                    this.countPage = events.count / this.limit + ( events.count % this.limit !== 0 ) ;
                }, e => {
                    alert('При получении даных произошла ошибка');
                })
            }
        },
        beforeMount() {
            this.getEvents()
        },
        computed: {
            ...mapGetters(['role'])
        }
    }
</script>