import axios from 'axios';

export default {

  getImage(id){
      return axios.get('/images/'+id)
  },
  updateImage(image){
    return axios.put('/images', image)
  }
}