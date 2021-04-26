<template>
  <div class="content">
    <div id="inner-white-block">
      <component :is='display'/>
    </div>
  </div>
</template>

<script>
import BreweryList from '@/components/BreweryList.vue';
import BeerList from '@/components/BeerList.vue';
import UserProfile from '@/components/UserProfile.vue';
import BreweryManagement from '@/components/BreweryManagement.vue';
import BeerManagement from '@/components/BeerManagement.vue';
import BreweryPage from '@/components/BreweryPage.vue';
import BeerPage from '@/components/BeerPage.vue';
import AnonymousUserPage from '@/components/AnonymousUserPage';

export default {
  data(){
    return{
    }
  },
  components: {
    BeerList,
    BreweryList,
    UserProfile,
    BreweryManagement,
    BeerManagement,
    BreweryPage,
    BeerPage,
    AnonymousUserPage
  },
  computed:{
    display(){
      switch(this.$store.state.currentDisplay){
        case 1:
          return "BreweryList"
        case 2:
          return "BeerList"
        case 3:
          return "BreweryManagement"
        case 4:
          return "BeerManagement"
          case 5:
            return "BreweryPage"
          case 6:
            return "BeerPage"
        default:
          return "AnonymousUserPage"
      }
    }
  }
};
</script>

<style scoped>
.content{
  display: flex;
  flex-direction: column;
  max-height: 100%;
  justify-content: center;
}

#inner-white-block{
  display: flex;
  flex-basis: 100%;
  height: 100%;
}
</style>