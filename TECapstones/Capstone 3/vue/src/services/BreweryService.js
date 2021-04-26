import axios from 'axios';

export default {

  getBreweries(){
      return axios.get('/brewery')
  },

  addBrewery(brewery){
    return axios.post('/brewery', brewery)
  },
  editBrewery(brewery){
    return axios.put('/brewery/'+brewery.breweryId, brewery)
  }
}