<script setup>
import {onMounted, ref} from "vue"
import HelloWorld from './components/HelloWorld.vue'
import TheWelcome from './components/TheWelcome.vue'
import DialogModal from './components/DialogModal.vue'
//import {conn, groupId} from './utils/signalr-connect'

const showDialog = ref(false)

onMounted(() => {
  
    //console.log('signalr conn11', conn.state)  
    

} )

const show = ()=>{  
  //console.log('showDialog1:', showDialog.value)
  showDialog.value = !showDialog.value
  //console.log('showDialog2:', showDialog.value)
}
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
      <button @click="show">Show </button>
      <DialogModal v-model="showDialog" />
    </div>
  </header>

  <main>
    <TheWelcome />
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
