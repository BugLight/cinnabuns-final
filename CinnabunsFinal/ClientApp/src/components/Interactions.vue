<template>
    <b-container fluid>
        <div class="title-row">
            <h1>Взаимодействие</h1>
            <b-button variant="success" @click="isModelNewInt = true">Создать взаимодействие</b-button>
        </div>
        <b-row>
            <b-col md="4">
                <b-list-group v-if="interactions">
                    <b-list-group-item style="cursor: pointer" v-for="int in interactions" @click="setCurrentTask(int)" :active="int == currentIntc">
                        {{int.date}}
                    </b-list-group-item>
                </b-list-group>
                <div v-else>
                    Задачи отсутствуют
                </div>
            </b-col>
            <b-col md="8">
                <b-card v-if="currentIntc">
                    <b-card-title>{{currentIntc.date}}</b-card-title>
                    <b-card-text>
                        <div>
                            <b>#</b>
                            {{currentIntc.id}}
                        </div>
                        <div>
                            <b>Дата: </b>
                            {{currentIntc.date}}
                        </div>
                        <div>
                            <b>Тип: </b>
                            {{currentIntc.type}}
                        </div>
                        <div>
                            <b>Ответственный: </b>
                            <span>{{currentIntc.responsible.name}} </span>
                            <span v-if="currentIntc.responsible.surname">
                                {{currentIntc.responsible.surname}} {{currentIntc.responsible.patronymic}}
                            </span>
                        </div>
                        <p class="task-description">
                            {{currentIntc.description}}
                        </p>
                    </b-card-text>

                    <b-button variant="success">Завершить</b-button>
                </b-card>
            </b-col>
        </b-row>
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
        <div v-bind:class="`modal ${isModelNewInt ? 'show' : 'fade'}`">
            <modal-create view="partners"></modal-create>
        </div>
    </b-container>
</template>

<script>
    export default {
        components: {
            ModalCreate: () => import('./ModalCreate.vue')
        },
        data() {
            return {
                currentIntc: null,
                interactions: null,
                limit: 50,
                offset: 0,
                countPage: 0,
                activePage: 1,
                isModelNewInt: false
            };
        },
        methods: {
            getInteractions() {
                this.$http.get('http://172.20.0.3/api/interactions')
                    .then(res => res.json())
                    .then(json => {
                        let interactions = json.data;
                        this.currentIntc = null;
                        this.interactions = interactions;
                    })
                    .catch(e => alert('При получении даных произошла ошибка'));
            },
            incPage: function() {
                if (this.activePage < this.countPage) {
                    this.activePage += 1;
                    this.offset += this.limit;
                    this.getInteractions()
                }
            },
            decPage: function() {
                if (this.activePage !== 1 && this.activePage > 1) {
                    this.activePage -= 1;
                    this.offset -= this.limit;
                    this.getInteractions()
                }
            },
            setCurrentTask(task) {
                this.currentIntc = task;
            }
        },
        beforeMount() {
            this.getInteractions();
        },
        mounted() {
            this.$on('fadeModal', (is) => {
                this.isModelNewInt = is;
                this.getInteractions()
            })
        },
    }
</script>