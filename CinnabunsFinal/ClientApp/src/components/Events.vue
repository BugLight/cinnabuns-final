<template>
    <div class="row">
        <div class="title-row">
            <h1>События</h1>
            <b-button variant="success" @click="isModelNewEvent = true">Создать новое событие</b-button>
        </div>
        <div class="table-responsive">
            <table class="table">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Название</th>
                        <th>Дата начала события</th>
                        <th>Дата окончания события</th>
                        <th>Редактирование</th>
                    </tr>
                </thead>
                <tbody v-if="events">
                    <tr v-for="event in events.data" :key="events.id">
                        <td>{{event.id}}</td>
                        <td>{{event.name}}</td>
                        <td>{{event.beginDate}}</td>
                        <td>{{event.endDate}}</td>
                        <td><font-awesome-icon icon="pen" color="#000" @click="deleteEvent(event)" style="margin-right: 50px"/><font-awesome-icon icon="trash" color="#000"/></td>
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
        <div v-bind:class="`modal ${isModelNewEvent ? 'show' : 'fade'}`">
            <modal-create view="events"></modal-create>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from "vuex";

    export default {
        components: {
            ModalCreate: () => import('./ModalCreate.vue')
        },
        data() {
            return {
                events: null,
                limit: 50,
                offset: 0,
                countPage: 0,
                activePage: 1,
                isModelNewEvent: false
            }
        },
        methods: {
            deleteEvent: function (event) {
                this.$http.delete(`/api/events/?${event.id}`).then(res => {
                    this.events.splice(this.events.indexOf(event), 1)
                })
            },
            incPage: function() {
                if (this.activePage < this.countPage) {
                    this.activePage += 1;
                    this.offset += this.limit;
                    this.getEvents()
                }
            },
            decPage: function() {
                if (this.activePage !== 1 && this.activePage > 1) {
                    this.activePage -= 1;
                    this.offset -= this.limit;
                    this.getEvents()
                }
            },
            getEvents: function () {
                this.$http.get(`/api/events/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                    this.events = res.body;
                    this.countPage = Math.floor(this.events['totalCount'] / this.limit + ( this.events['totalCount'] % this.limit !== 0 ? 1 : 0));
                }, e => {
                    alert('При получении даных произошла ошибка');
                })
            }
        },
        beforeMount() {
            this.getEvents()
        },
        mounted() {
            this.$on('fadeModal', (is) => {
                this.isModelNewEvent = is
            })
        },
        computed: {
            ...mapGetters(['role'])
        }
    }
</script>

<style lang="scss">
    .title-row {
        margin: 20px;
    }
    .spinner--block {
        margin: 40px;
    }
    .show {
        display: block;
    }
    .page-link {
        cursor: pointer;
    }
</style>