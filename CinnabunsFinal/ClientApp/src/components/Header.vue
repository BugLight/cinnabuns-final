<template>
    <div class="row">
        <div class="col col-xl" @click="$router.push('/')">Главная страница</div>
        <div class="col col-xl-" @click="$router.push('/events')">Мероприятия</div>
        <div class="col col-xl-" @click="$router.push('/partners')">Партнеры</div>
    </div>
</template>

<style lang="scss" scoped>
    .row {
        height: 70px;
        .col {
            text-align: center;
            cursor: pointer;
            padding-top: 15px;
            font-size: 23px;
            &:hover {
                background-color: rgba(255, 202, 191, 0.25);
            }
        }
    }
</style>