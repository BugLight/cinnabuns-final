<template>
    <b-container fluid>
        <div class="title-row">
            <h1>Задачи</h1>
            <b-button variant="success" @click="isModelNewTask = true">Создать задачу</b-button>
        </div>
        <b-row>
            <b-col md="4">
                <b-form-select :options="taskOptions" v-model="filterMyTasks" @change="getTasks">
                </b-form-select>
                <b-list-group v-if="tasks">
                    <b-list-group-item style="cursor: pointer" v-for="task in tasks" @click="setCurrentTask(task)" :active="task == currentTask">
                        {{task.name}} <span v-if="task.completed">(Выполнена)</span>
                    </b-list-group-item>
                </b-list-group>
                <div v-else>
                    Задачи отсутствуют
                </div>
            </b-col>
            <b-col md="8">
                <b-card v-if="currentTask">
                    <b-card-title>{{currentTask.name}}</b-card-title>
                    <b-card-text>
                        <div>
                            <b>Организатор: </b>
                            {{currentTask.assigner.userName}}
                        </div>
                        <div>
                            <b>Ответственный: </b>
                            {{currentTask.responsible.userName}}
                        </div>
                        <div>
                            <b>Выполнить до: </b>
                            {{currentTask.endDate}}
                        </div>
                        <div>
                            <b>Связанное событие: </b>
                            {{currentTask.event.name}}
                        </div>
                        <div>
                            <b>Партнер: </b>
                            <span>{{currentTask.partner.name}} </span>
                            <span v-if="currentTask.partner.surname">
                                {{currentTask.partner.surname}} {{currentTask.partner.patronymic}}
                            </span>
                        </div>
                        <p class="task-description">
                            {{currentTask.description}}
                        </p>
                    </b-card-text>

                    <b-button variant="success" v-if="!currentTask.completed" @click="completeTask">Завершить</b-button>
                </b-card>
            </b-col>
        </b-row>
        <div v-bind:class="`modal ${isModelNewTask ? 'show' : 'fade'}`">
            <modal-create view="tasks"></modal-create>
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
                currentTask: null,
                tasks: null,
                isModelNewTask: false,
                filterMyTasks: true,
                taskOptions: [
                    { text: 'Мои задачи', value: true },
                    { text: 'Все задачи', value: false }
                ]
            };
        },
        methods: {
            getTasks() {
                let uid = this.$store.getters.uid;
                this.$http.get('/api/tasks' + this.filterMyTasks ? `?responsibleId=${uid}` : '')
                    .then(res => res.json())
                    .then(json => {
                        let tasks = json.data;
                        this.currentTask = null;
                        this.tasks = tasks;
                    })
                    .catch(e => alert('При получении даных произошла ошибка'));
            },
            setCurrentTask(task) {
                this.currentTask = task;
                this.$http.get(`/api/partners/${task.partnerId}`)
                    .then(res => res.json())
                    .then(partner => {
                        this.currentTask.partner = partner;
                        return this.$http.get(`/api/events/${task.eventId}`);
                    })
                    .then(res => res.json())
                    .then(event => {
                        this.currentTask.event = event;
                    })
                    .catch(e => alert('При получении даных произошла ошибка'));
            },
            completeTask() {
                this.$http.post(`/api/tasks/${this.currentTask.id}/complete`)
                    .then(res => this.currentTask.completed = true)
                    .catch(e => alert('При получении даных произошла ошибка'));
            }
        },
        beforeMount() {
            this.getTasks();
        },
        mounted() {
            this.$on('fadeModal', (is) => {
                this.isModelNewTask = is;
                this.getTasks();
            })
        },
    }
</script>

<style scoped>
    .task-description {
        margin-top: 15px;
    }

    .modal {
        overflow: scroll;
    }
</style>