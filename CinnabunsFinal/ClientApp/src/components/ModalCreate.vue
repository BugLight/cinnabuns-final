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
                    <div class="tags-help" v-if="field.type === 'tags'">
                        <input class="form-control" type="text" @keyup="helpTags" v-model="model.helpT" @focus="showHelp" @blur="fadeHelp"> 
                        <div class="help-array form-group" id="help-array">
                            <span class="form-control" style="font-size: 13px;" v-for="help in model.helpMe">{{}}</span>
                        </div>
                    </div>
                    <input class="form-control" v-else :type="field.type" :id="field.filedId" :placeholder="field.name" v-model="model[field.model]"/>
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
                    helpMe: null,
                    helpT: '',
                    name: '',
                    beginDate: '',
                    endDate: '',
                    description: '',
                    responsibleId: '',
                    eventId: '',
                    partnerId: ''
                    inn: '',
                    site: '',
                    surname: '',
                    email: '',
                    patronymic: '',
                    phone: ''
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
                        title: 'Создание нового партнера',
                        fields: [
                            {
                                name: 'Название',
                                type: 'text',
                                filedId: 'field-name',
                                model: 'name'
                            },
                            {
                                name: 'ИНН',
                                type: 'number',
                                filedId: 'field-inn',
                                model: 'inn'
                            },
                            {
                                name: 'Web-site',
                                type: 'text',
                                filedId: 'field-site',
                                model: 'site'
                            },
                            {
                                name: 'Фамилия',
                                type: 'text',
                                filedId: 'field-surname',
                                model: 'surname'
                            },
                            {
                                name: 'Отчество',
                                type: 'text',
                                filedId: 'field-patronymic',
                                model: 'patronymic'
                            },
                            {
                                name: 'Телефон',
                                type: 'text',
                                filedId: 'field-phone',
                                model: 'phone'
                            },
                            {
                                name: 'Email',
                                type: 'mail',
                                filedId: 'field-mail',
                                model: 'email'
                            },
                            {
                                name: 'Описание',
                                type: 'text',
                                filedId: 'field-description',
                                model: 'description'
                            },
                        ]
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
            fadeHelp: function() {
                document.getElementById('help-array').style.display = 'none'
            },
            showHelp: function() {
                if (this.model.helpMe) {
                    document.getElementById('help-array').style.display = 'block'
                }
            },
            helpTags: function() {
                this.$http.get(`http://172.20.0.3/api/tags/search?q=${this.model.helpT}`).then(res => {
                    this.model.helpMe = res.body;
                    if (this.model.helpMe.length > 0) {
                        document.getElementById('help-array').style.display = 'block'
                    }
                })
            },
            closeModal: function(){
                this.$parent.$emit('fadeModal', false)
            },
            createObject: function() {
                if (this.view === 'events') {
                    this.$http.post('http://172.20.0.3/api/events', {
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
                } else if (this.view === 'partners') {
                    this.$http.post('http://172.20.0.3/api/partners', {
                        name: this.model.name,
                        inn: this.model.inn,
                        site: this.model.site,
                        surname: this.model.surname,
                        patronymic: this.model.patronymic,
                        phone: this.model.phone,
                        email: this.model.email,
                        description: this.model.description
                    }).then(res => {
                        alert('Создание прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время создания произошла ошибка')
                    });
                }
            }
        }
    }
</script>
<style lang="scss">
    /*.tags-help {*/
        /*position: relative;*/
    /*}*/
    .help-array {
        overflow-y: scroll;
        height: 100px;
        display: none;
        .form-control {
            z-index: 1050;
            font-size: 17px;
        }
    }
</style>
