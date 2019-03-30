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
                    <b-spinner style="width: 5rem; height: 5rem;" label="Large Spinner"></b-spinner>
                </div>
            </table>
        </div>
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
                countPage: 0
            }
        },
        methods: {
            
        },
        created() {
            this.$http.get(`/api/events/?limit=${this.limit}&offset=${this.offset}`).then(res => {
                this.events = res.body;
                this.countPage = events.count / this.limit + ( events.count % this.limit !== 0 ) ;
            }, e => {
                alert('При получении даных произошла ошибка');
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
</style>