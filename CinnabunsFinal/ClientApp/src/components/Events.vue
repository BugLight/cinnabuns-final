<template>
    <div class="row">
        <div class="title-row">
            <h1>События</h1>
            <b-button variant="success">Создать новое событие</b-button>
        </div>
        <div class="table-responsive">
            <table class="table">
                <tr>
                    <th>#</th>
                    <th>Название</th>
                    <th>Дата начала события</th>
                    <th>Дата окончания события</th>
                    <th>Редактирование</th>
                </tr>
                <div v-if="events">
                    <tr v-for="event in events" :key="events.id">
                        <td>{{event.name}}</td>
                        <td>{{event.begin_date}}</td>
                        <td>{{event.end_date}}</td>
                        <td><font-awesome-icon icon="pen" color="#000"/></td>
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
                events: null,
                limit: 50,
                offset: 0,
                countPage: 10,
                activePage: 1
            }
        },
        methods: {
            selectPage: function (i) {
                this.activePage = i;
                this.getEvents()
            },
            getEvents: function () {
                this.$http.get(`/api/events/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                    this.events = res.body;
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

<style lang="scss">
    .title-row {
        margin: 20px;
    }
    .spinner--block {
        margin: 40px;
    }
</style>