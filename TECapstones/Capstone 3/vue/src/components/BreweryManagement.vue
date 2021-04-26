<template>
  <div id='breweryManagement'>
    <div id="MainContent">
        <div class="inner-block">
            <add-brewery-form></add-brewery-form>
            <h1>Select Brewery</h1>
            <div id='managebrewerylist' v-for="brewery in breweries" :key="brewery.breweryId" @click='chooseBrewery(brewery.breweryId)'>
                <h1 >{{brewery.name}}</h1>
            </div>
        </div>
    </div>
    <side-details :current-brewery='currentBrewery' v-if='showSide'/>
  </div>
</template>

<script>
import SideDetails from '@/components/SideDetails.vue'
import AddBreweryForm from '@/components/AddBreweryForm.vue'
export default {
    data(){
        return{
    showSide: false,
    currentBrewery : -1,
    user:{}
    }
    },
    components: {
        SideDetails,
        AddBreweryForm
    },
    methods:{
        chooseBrewery(id){
            this.$store.state.showEditForm = false
            this.$store.state.showAddForm = false
            this.$store.state.showPictureForm = false
            this.currentBrewery = id
            this.showSide = true
            this.$store.state.currentBrewery = id
            this.$store.commit('SET_EDITING_MODE',1);

        }
    },
    computed:{
        breweries(){
            return this.$store.state.breweries.filter(brew => (brew.brewerId===this.$store.state.user.userId))
        }
    },
    created(){
        
        this.user = this.$store.state.user
        }
    
}
</script>

<style >
#breweryManagement{
    display: flex;
    flex-direction: row;
    align-items:stretch;
    border-radius: 10px;
    flex-basis: 100%;
}
#MainContent{
    display: flex;
    flex-direction: column;
    background-color: rgba(53, 53, 53, 0.8);
    border-radius: 10px;
    padding: 1rem;
    justify-content: center;
    margin-right: 20px;
    flex-basis: 60%;
}

#managebrewerylist{
    display: flex;
    border: 2px solid black;
    border-radius: 10px;
    padding: 1rem;
    margin-bottom:10px;
    justify-content: center;
}
#managebrewerylist >h1{
    display: flex;
}
#managebrewerylist:hover{
    background-color: rgb(176, 179, 182);
    box-shadow: 5px 5px 3px black;
}

.inner-block{
    display: flex;
    text-align: center;
    overflow: auto;
}

h1 :hover
{
 color: yellow;
}
</style>
