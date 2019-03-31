<template>
    <div class="row">
        <div class="title-row">
            <h1>Партнеры</h1>
            <b-button variant="success" @click="isModelNewPartner = true">Создать нового партнера</b-button>
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
                    <tr v-for="partner in partners.data" :key="partner.id">
                        <td>{{partner.name}}</td>
                        <td><font-awesome-icon icon="pen" color="#000"/> <font-awesome-icon icon="trash" color="#000" @click="deletePartner(partner)"/></td>
                    </tr>
                </div>
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
        <div v-bind:class="`modal ${isModelNewPartner ? 'show' : 'fade'}`">
            <modal-create view="partners"></modal-create>
        </div>
        <div v-bind:class="`modal ${isModalUpPartner ? 'show' : 'fade'}`">
            <modal-update view="partners" :pattern="updateP"></modal-update>
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
                partners: null,
                limit: 50,
                offset: 0,
                countPage: 0,
                activePage: 1,
                isModelNewPartner: false,
                isModalUpPartner: false,
                updateP: null
            }
        },
        methods: {
            updateParnter: function (event) {
                this.updateP = event;
                this.isModalUpPartner = true;
            },
            deletePartner: function (partner) {
                this.$http.delete(`http://172.20.0.3/api/partners/?${partner.id}`).then(res => {
                    this.partners.splice(this.partners.indexOf(partner), 1)
                })
            },
            incPage: function() {
                if (this.activePage < this.countPage) {
                    this.activePage += 1;
                    this.offset += this.limit;
                    this.getPartner()
                }
            },
            decPage: function() {
                if (this.activePage !== 1 && this.activePage > 1) {
                    this.activePage -= 1;
                    this.offset -= this.limit;
                    this.getPartner()
                }
            },
            getPartner: function () {
                this.$http.get(`http://172.20.0.3/api/partners/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                    this.partners = res.body;
                    this.countPage = Math.floor(this.partners['totalCount'] / this.limit + ( this.partners['totalCount'] % this.limit !== 0 ? 1 : 0));
                }, e => {
                    alert('При получении даных произошла ошибка');
                })
            }
        },
        beforeMount() {
            this.getPartner()
        },
        mounted() {
            this.$on('fadeModal', (is) => {
                this.isModelNewPartner = is;
                this.isModalUpPartner = is;
                this.getPartner()
            })
        },
        computed: {
            ...mapGetters(['role'])
        }
    }
</script>