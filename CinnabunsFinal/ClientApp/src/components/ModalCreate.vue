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
                    <b-form-select v-else-if="field.type === 'select'" v-model="model.role">
                        <template slot="first">
                            <option :value="null" disabled>Выберите роль</option>
                        </template>
                        <option value="admin">Администратор</option>
                        <option value="organizer">Организатор</option>
                        <option value="volunteer">Волонтер</option>
                    </b-form-select>
                    <b-form-select v-else-if="field.type === 'select2'" v-model="model.type">
                        <template slot="first">
                            <option :value="null" disabled>Тип взаимодействия</option>
                        </template>
                        <option value="0">Телефонный звокнок</option>
                        <option value="1">Email</option>
                        <option value="2">Личная встреча</option>
                        <option value="3">Другое</option>
                    </b-form-select>
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
                    contactId: '',
                    eventId: '',
                    partnerId: '',
                    inn: '',
                    site: '',
                    surname: '',
                    email: '',
                    patronymic: '',
                    phone: '',
                    role: '',
                    date: '',
                    type: ''
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
                        title: 'Создание нового пользователя',
                        fields: [
                            {
                                name: 'Имя',
                                type: 'text',
                                filedId: 'field-name',
                                model: 'name'
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
                                type: 'text',
                                filedId: 'field-email',
                                model: 'email'
                            },
                            {
                                name: 'Роль',
                                type: 'select',
                                filedId: 'field-role',
                                model: 'role'
                            },
                        ]
                    },
                    'interactions': {
                        title: 'Создание нового взаимодействия',
                        fields: [
                            {
                                name: 'Дата',
                                type: 'date',
                                fieldId: 'filed-date',
                                model: 'date'
                            },
                            {
                                name: 'Тип',
                                type: 'select2',
                                fieldId: 'filed-select',
                                model: 'type'
                            },
                            {
                                name: 'Описание',
                                type: 'test',
                                fieldId: 'filed-description',
                                model: 'description'
                            },
                            {
                                name: 'Ответственный Id',
                                type: 'text',
                                fieldId: 'filed-res-id',
                                model: 'responsibleId'
                            },
                            {
                                name: 'Id контакта взаимодействия',
                                type: 'text',
                                fieldId: 'filed-con-id',
                                model: 'contactId'
                            }
                        ]
                    }
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
                this.$http.get(`/api/tags/search?q=${this.model.helpT}`).then(res => {
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
                } else if (this.view === 'partners') {
                    this.$http.post('/api/partners', {
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
                    })
                } else if (this.view === 'users') {
                    this.$http.post(`/api/auth/register?role=${this.model.role}`, {
                        name: this.model.name,
                        surname: this.model.surname,
                        patronymic: this.model.patronymic,
                        phone: this.model.phone,
                        email: this.model.email,
                    }).then(res => {
                        alert('Создание прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время создания произошла ошибка')
                    })
                } else if (this.view === 'interactions') {
                    this.$http.post(`/api/interactions`, {
                        date: this.model.date,
                        type: this.model.type,
                        description: this.model.description,
                        responsibleId: this.model.responsibleId,
                        contactId: this.model.contactId,
                    }).then(res => {
                        alert('Создание прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время создания произошла ошибка')
                    })
                }
            }
        },
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
