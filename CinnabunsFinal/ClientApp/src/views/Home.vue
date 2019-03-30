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
                    <h5>Оказание комплексной помощи взрослым онкобольным людям.</h5>
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