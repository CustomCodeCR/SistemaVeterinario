import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://api.vetfriends.customcodecr.com/api/v1',
  headers: {
    'Content-Type': 'application/json'
  }
});

export default axiosInstance;
