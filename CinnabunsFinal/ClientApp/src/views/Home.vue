<template>
    <div class="container-fluid">
        <Header></Header>
        <div class="row">
            <div class="col-12 col-sm-12">
                <div class="title-icon"></div>
            </div>
            <div class="cont-info">
                <div class="col-10">
                    <h2>Информация об организации</h2>
                    <p>Рак – серьезное и опасное заболевание, которое является
                        медико-социальной проблемой. Показатели смертности и заболеваемости
                        во всем мире ежегодно возрастают, что связано с изменением образа
                        жизни, ухудшением экологической обстановки и увеличением влияния
                        внешних и внутренних неблагоприятных факторов.
                        Одно из направлений деятельности Федерации «Огромное сердце» –
                        организация мероприятий (выставки, тренинги, семинары) как с целью
                        сбора средств, так и для поддержки онкобольных.
                        Каждое мероприятие, по своей сути – это мини-проект, в рамках
                        которого требуется договориться с целым рядом партнеров: найти
                        площадку для проведения мероприятия, спикеров, подготовить сувенирную
                        продукцию, организовать фото/видеосъемку и освещение события в
                        различных СМИ и т.п.
                        Для облегчения организации мероприятий была создана данная система.
                        Мы надеемся на успешное внедрение системы в работу Федерации и успехи с ее помощью.</p>
                </div>
                <div class="col-md-6 col-md-offset-3">
                    <div class="lead">Наши контакты:</div>
                    <a href="tel: 84957733067"><font-awesome-icon icon="phone" color="#000"/> 8 (495) 773-30-67</a><br>
                    <a href="https://m.me/GreatHeartFederation"><img src="../assets/facebook-messenger-brands.svg" width="16"> m.me/GreatHeartFederation</a><br>
                    <a href="mailto: info@federaciya.com"><font-awesome-icon icon="paper-plane" color="#000"/> info@federaciya.com</a>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        components: {
            Header: () => import('../components/Header.vue')
        }
    }
</script>

<style lang="scss">
    .cont-info {
        width: 80%;
        margin: 0 auto;
    }
    .title-icon {
        margin: 40px auto;
        width: 500px;
        background-image: url('../assets/logo_federation-01.png');
        background-size: 100%;
        background-repeat: no-repeat;
        height: 150px;
    }
</style>
