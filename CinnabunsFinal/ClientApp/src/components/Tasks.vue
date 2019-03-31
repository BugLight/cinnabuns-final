<template>
    <b-container fluid>
        <div class="title-row">
            <h1>Задачи</h1>
        </div>
        <b-row>
            <b-col md="4">
                <b-list-group v-if="tasks">
                    <b-list-group-item style="cursor: pointer" v-for="task in tasks" @click="setCurrentTask(task)" :active="task == currentTask">
                        {{task.name}}
                    </b-list-group-item>
                </b-list-group>
                <div v-else>
                    Задачи отсутствуют
                </div>
            </b-col>
            <b-col md="8">
                <b-card v-if="currentTask">
                    <b-card-title>{{currentTask.name}}</b-card-title>
                    <b-card-text>{{currentTask.description}}</b-card-text>
                </b-card>
            </b-col>
        </b-row>
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
                tasks: null
            };
        },
        methods: {
            getTasks() {
                this.$http.get('/api/tasks')
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
            }
        },
        beforeMount() {
            this.getTasks();
        }
    }
</script>

<style scoped>

</style>