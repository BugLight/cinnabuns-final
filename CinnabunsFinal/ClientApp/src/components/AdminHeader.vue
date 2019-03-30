<template>
    <div class="row">
        <div class="col col-xl" v-for="action in actions" :key="action.id" @click="$store.commit('setActiveAdminPage', action.name)">{{action.title}}</div>
    </div>
</template>

<script>
    import { mapGetters } from "vuex";
    
    export default {
        computed: {
            ...mapGetters(['actions'])
        }
    }
</script>

<style lang="scss" scoped>
    .row {
        height: 80px;
        background-color: rgba(255,202,191,0.79);
        .col {
            text-align: center;
            cursor: pointer;
            padding-top: 15px;
            font-size: 23px;
            &:hover {
                background-color: rgb(255, 240, 249);
            }
        }
    }
</style>