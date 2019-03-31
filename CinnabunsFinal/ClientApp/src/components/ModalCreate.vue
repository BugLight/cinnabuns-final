<template>
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{action[view].title}}</h4>
                <button type="button" class="close" @click="closeModal">×</button>
            </div>
            <div class="modal-body">
                <div class="form-group" v-for="field in action[view].fields" :key="field.id">
                    <label :for="field.filedId">{{field.name}}</label>
                    <input class="form-control" :type="field.type" :id="field.filedId" :placeholder="field.name" v-model="model[field.model]"/>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" @click="closeModal">Закрыть</button>
                <button type="button" class="btn btn-primary" @click="createObject">Сохранить изменения</button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                model: {
                    name: '',
                    beginDate: '',
                    endDate: '',
                    description: '',
                    responsibleId: '',
                    eventId: '',
                    partnerId: ''
                },
                action: {
                    'events': {
                        title: 'Создание нового события',
                        fields: [
                            {
                                name: 'Название',
                                type: 'text',
                                filedId: 'field-name',
                                model: 'name'
                            },
                            {
                                name: 'Дата начала',
                                type: 'date',
                                filedId: 'field-begin_date',
                                model: 'beginDate'
                            },
                            {
                                name: 'Дата окончания',
                                type: 'date',
                                filedId: 'field-end_date',
                                model: 'endDate'
                            },
                            {
                                name: 'Описание',
                                type: 'text',
                                filedId: 'field-description',
                                model: 'description'
                            }
                        ]
                    },
                    'tasks': {
                        title: 'Создание новой задачи',
                        fields: [
                            {
                                name: 'Название',
                                type: 'text',
                                filedId: 'field-name',
                                model: 'name'
                            },
                            {
                                name: 'Выполнить до',
                                type: 'date',
                                filedId: 'field-end_date',
                                model: 'endDate'
                            },
                            {
                                name: 'Ответственный Id',
                                type: 'text',
                                filedId: 'field-responsible_id',
                                model: 'responsibleId'
                            },
                            {
                                name: 'Событие Id',
                                type: 'text',
                                filedId: 'field-event_id',
                                model: 'eventId'
                            },
                            {
                                name: 'Партнер Id',
                                type: 'text',
                                filedId: 'field-partner_id',
                                model: 'partnerId'
                            },
                            {
                                name: 'Описание',
                                type: 'text',
                                filedId: 'field-description',
                                model: 'description'
                            }
                        ]
                    },
                    'partners': {
                        title: 'Создание нового партнера'
                    },
                    'tags': {
                        title: 'Создание нового теги'
                    },
                    'users': {
                        title: 'Создание нового пользователя'
                    },
                }
            }
        },
        props: {
            view: {
                type: String,
                default: null
            }
        },
        methods: {
            closeModal: function(){
                this.$parent.$emit('fadeModal', false)
            },
            createObject: function() {
                if (this.view === 'events') {
                    this.$http.post('/api/events', {
                        name: this.model.name,
                        beginDate: this.model.beginDate,
                        endDate: this.model.endDate,
                        description: this.model.description
                    }).then(res => {
                        alert('Создание прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время создания произошла ошибка')
                    })
                } else if (this.view === 'tasks') {
                    this.$http.post('/api/tasks', {
                        name: this.model.name,
                        endDate: this.model.endDate,
                        responsibleId: this.model.responsibleId,
                        eventId: this.model.eventId,
                        partnerId: this.model.partnerId,
                        description: this.model.description
                    }).then(res => {
                        alert('Задача создана');
                        this.closeModal();
                    }, e => {
                        alert('Во время создания произошла ошибка');
                    });
                }
            }
        }
    }
</script>