import axios from "axios";

export default class CaptchaService {
    static postNewKey() {
        return axios.post('/api/CaptchaImage/post-guid-captcha')
    };
}