import axios from 'axios';

export default {

  getBeers(){
      return axios.get('/beer')
  },

  addBeer(beer){
    return axios.post('/beer', beer)
  },
  editBeer(beer){
    return axios.put('/beer/'+beer.beerId+'/update', beer)
  },
  deleteBeer(beer){
      return axios.put('/beer/'+beer.beerId+'/delete', beer)
  }

}