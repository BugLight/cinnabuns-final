<template>
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title">{{action[view].title}}</h4>
                <button type="button" class="close" @click="closeModal">×</button>
            </div>
            <div class="modal-body" v-if="copyPattern">
                <div class="form-group" v-for="field in action[view].fields" :key="field.id">
                    <label :for="field.filedId">{{field.name}}</label>
                    <input class="form-control" :type="field.type" :id="field.filedId" :placeholder="field.name" v-model="copyPattern[field.model]" v-if="field.type !== 'date'"/>
                    <date-picker class="only-datepicker" v-else v-model="copyPattern[field.model]"></date-picker>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" @click="closeModal">Закрыть</button>
                <button type="button" class="btn btn-primary" @click="updateObject">Сохранить изменения</button>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                copyPattern: null,
                action: {
                    'events': {
                        title: 'Изменение события',
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
                        title: 'Создание новой задачи'
                    },
                    'partners': {
                        title: 'Изменение партнера',
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
            },
            pattern: {
                type: Object,
                default: null
            }
        },
        watch: {
            pattern: function() {
                this.copyPattern = Object.assign({}, this.pattern);
            }
        },
        methods: {
            closeModal: function(){
                this.$parent.$emit('fadeModal', false)
            },
            updateObject: function() {
                if (this.view === 'events') {
                    this.$http.put(`/api/events/${this.copyPattern.id}`, {
                        name: this.copyPattern.name,
                        beginDate: this.copyPattern.beginDate,
                        endDate: this.copyPattern.endDate,
                        description: this.copyPattern.description
                    }).then(res => {
                        alert('Изминение прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время изминения произошла ошибка');
                    })
                } else if (this.view === 'partners') {
                    this.$http.put(`/api/partners/${this.copyPattern.id}`, {
                        name: this.copyPattern.name,
                        inn: this.copyPattern.inn,
                        site: this.copyPattern.site,
                        surname: this.copyPattern.surname,
                        patronymic: this.copyPattern.patronymic,
                        phone: this.copyPattern.phone,
                        email: this.copyPattern.email,
                        description: this.copyPattern.description
                    }).then(res => {
                        alert('Изменение прошло успешно');
                        this.closeModal()
                    }, e => {
                        alert('Во время изменения произошла ошибка')
                    })
                }
            }
        }
    }
</script>

<style lang="scss">
    .only-datepicker {
        div {
            input[type='text'] {
                display: block;
                width: 100%;
                height: calc(1.5em + 0.75rem + 2px);
                padding: 0.375rem 0.75rem;
                font-size: 1rem;
                font-weight: 400;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid #ced4da;
                border-radius: 0.25rem;
            }
        }
    }
</style>