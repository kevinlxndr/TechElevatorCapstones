<template>
<div>
    <button id="add-brewery" v-if="!$store.state.showAddForm" @click.prevent="spawnForm">
            Add A Brewery
    </button>
    <form class="breweryform" @submit.prevent v-if="$store.state.showAddForm">
        <div class="form-element">
            <label for="name">Name</label>
            <input type="text" id="nameEntry" placeholder = "Name" v-model="newBrewery.Name" required>
        </div>
        <div class="form-element">
            <label for="streetaddress1">Street Address 1:</label>
            <input type="text" id="streetaddress1" placeholder="Street Address 1" v-model="newBrewery.StreetAddress1" required>
        </div>
        <div class="form-element">  
            <label for="streetaddress2">Street Address 2:</label>
            <input type="text" id="streetaddress2" v-model="newBrewery.StreetAddress2">
        </div>
        <div class="form-element">
            <label for="city">City</label>
            <input type="text" id="city" placeholder="City" v-model="newBrewery.City" required>
        </div>
        <div class="form-element">
            <label for="state">State</label>
            <input type="text" id="state" placeholder="State" v-model="newBrewery.State" required>
        </div>
        <div class="form-element">
            <label for="zip">Zip Code</label>
            <input type="text" id="zip" placeholder="Zip" v-model.number="newBrewery.Zip" required>
        </div>
        <div class="form-element">
            <label for="phone">Phone</label>
            <input type="text" id="phone" placeholder="Phone" v-model="newBrewery.Phone" required>
        </div>
         <div class="form-element">  
            <label for="history">History</label>
            <textarea name="history" id="history" cols="30" rows="10" placeholder="Add Brewery History" v-model="newBrewery.History" required></textarea>
        </div>
        <div class="form-element">  
            <label for="hours">Hours of Operation</label>
            <textarea name="hours" id="hours" cols="30" rows="10" placeholder="Add Hours of Operation" v-model="newBrewery.HoursOfOperation" required></textarea>
        </div>
        <div class="form-element">
            <label for="website">Website</label>
            <input type="text" id="website" placeholder="URL" v-model="newBrewery.Website" required>
        </div>
        <div class="form-element">
            <input type="submit" value="Submit" @click ="addBrewery"/>
            <input type="button" value="Cancel" @click.prevent="resetForm" />
        </div>
       
        
    </form>
</div>
</template>

<script>
import BreweryService from '../services/BreweryService.js'
export default {
    name:"add-brewery",
    data(){
        return{
            newBrewery:{
                Name: "",
                BrewerId: this.$store.state.user.userId,
                StreetAddress1: "",
                StreetAddress2: "",
                City: "",
                State: "",
                Zip: "",
                Phone: "",
                History: "",
                HoursOfOperation: "",
                Website:"",
                BreweryStatus: 1
            }
        };
    },
    methods:{
        resetForm() {
            this.$store.state.showAddForm = false;
            this.newBrewery = { 
                Name: "",
                BrewerId: this.$store.state.user.userId,
                StreetAddress1: "",
                StreetAddress2: "",
                City: "",
                State: "",
                Zip: "",
                Phone: "",
                History: "",
                HoursOfOperation:"",
                Website:"",
                BreweryStatus: 1
            };
        },
        addBrewery(){
            BreweryService
            .addBrewery(this.newBrewery)
            .then(response => {
                if (response.status === 201) {
                    this.$store.state.showAddForm = false;
                    BreweryService.getBreweries().then(response => {
                    this.$store.state.breweries =  response.data;})
                }
            })
            .catch(error => {
                console.log(error);
            });
        },
        spawnForm(){
            this.$store.state.showAddForm = true;
            this.$store.state.showEditForm = false
        }
    }
}
</script>

<style>

</style>