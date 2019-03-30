<template>
    <div class="container-fluid">
        <Header></Header>
    </div>
</template>

<script>
    export default {
        components: {
            Header: () => import('../components/Header.vue')
        }
    }
</script>
