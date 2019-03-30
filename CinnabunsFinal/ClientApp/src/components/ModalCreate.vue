<template>
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{action[view].title}}</h4>
                <button type="button" class="close" @click="closeModal">×</button>
            </div>
            <div class="modal-body">
                <div class="form-group" v-for="field in action[view].fileds" :key="field.id">
                    <label :for="field.filedId">{{field.name}}</label>
                    <input class="form-control" :type="field.type" :id="field.filedId" :placeholder="field.name"/>
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
                },
                action: {
                    'events': {
                        title: 'Создание нового события',
                        fileds: [
                            {
                                name: 'Название',
                                type: 'text',
                                filedId: 'field-name',
                                model: this.name
                            },
                            {
                                name: 'Дата начала',
                                type: 'date',
                                filedId: 'field-begin_date',
                                model: this.beginDate
                            },
                            {
                                name: 'Дата окончания',
                                type: 'date',
                                filedId: 'field-end_date',
                                model: this.endDate
                            },
                            {
                                name: 'Описание',
                                type: 'text',
                                filedId: 'field-description',
                                model: this.description
                            }
                        ]
                    },
                    'tasks': {
                        title: 'Создание новой задачи'
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
                        name: this.name,
                        begin_date: this.beginDate,
                        end_date: this.endDate,
                        description: this.description
                    }).then(res => {
                        alert('Создание прошло успешно');
                        this.closeModal(    )
                    }, e => {
                        alert('Во время создания произошла ошибка')
                    })
                }
            }
        }
    }
</script>