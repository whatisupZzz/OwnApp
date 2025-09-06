import { defineStore } from 'pinia';
import { login as loginApi } from '@/api/user';
import { LoginParams } from '@/types/user';

interface UserState {
  token: string;
  userInfo: any;
  isLogin: boolean;
}

export const useUserStore = defineStore({
  id: 'user',
  state: (): UserState => ({
    token: localStorage.getItem('token') || '',
    userInfo: JSON.parse(localStorage.getItem('userInfo') || '{}'),
    isLogin: !!localStorage.getItem('token')
  }),
  getters: {
    getToken(): string {
      return this.token;
    },
    getUserInfo(): any {
      return this.userInfo;
    },
    getLoginStatus(): boolean {
      return this.isLogin;
    }
  },
  actions: {
    // 设置token
    setToken(token: string) {
      this.token = token;
      localStorage.setItem('token', token);
    },
    // 设置用户信息
    setUserInfo(userInfo: any) {
      this.userInfo = userInfo;
      localStorage.setItem('userInfo', JSON.stringify(userInfo));
    },
    // 设置登录状态
    setLoginStatus(status: boolean) {
      this.isLogin = status;
    },
    // 登录
    async login(params: LoginParams) {
      try {
        const { data } = await loginApi(params);
        this.setToken(data.token);
        this.setUserInfo(data.userInfo);
        this.setLoginStatus(true);
        return Promise.resolve(data);
      } catch (error) {
        return Promise.reject(error);
      }
    },
    // 登出
    logout() {
      this.setToken('');
      this.setUserInfo({});
      this.setLoginStatus(false);
      localStorage.removeItem('token');
      localStorage.removeItem('userInfo');
    }
  }
});