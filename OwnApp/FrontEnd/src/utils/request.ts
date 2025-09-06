import axios, { AxiosRequestConfig, AxiosResponse, AxiosError } from 'axios';
import { useUserStore } from '@/store/modules/user';

// 创建axios实例
const service = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api', // API的base_url
  timeout: 15000, // 请求超时时间
  headers: {
    'Content-Type': 'application/json;charset=utf-8'
  }
});

// 请求拦截器
service.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    const userStore = useUserStore();
    const token = userStore.getToken;
    if (token && config.headers) {
      // 让每个请求携带token
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error: AxiosError) => {
    console.error('请求错误:', error);
    return Promise.reject(error);
  }
);

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse) => {
    const res = response.data;
    // 如果返回的状态码不是200，说明接口请求有问题，直接抛出错误
    if (res.code !== 200) {
      // 处理token过期或无效
      if (res.code === 401 || res.code === 403) {
        const userStore = useUserStore();
        userStore.logout();
        window.location.href = '/login';
      }
      return Promise.reject(new Error(res.message || '未知错误'));
    } else {
      return res;
    }
  },
  (error: AxiosError) => {
    console.error('响应错误:', error);
    // 处理网络错误
    let message = '网络错误，请稍后重试';
    if (error.response) {
      switch (error.response.status) {
        case 401:
          message = '未授权，请重新登录';
          // 清除用户信息并跳转到登录页
          const userStore = useUserStore();
          userStore.logout();
          window.location.href = '/login';
          break;
        case 403:
          message = '拒绝访问';
          break;
        case 404:
          message = '请求的资源不存在';
          break;
        case 500:
          message = '服务器内部错误';
          break;
        default:
          message = `未知错误: ${error.response.status}`;
      }
    }
    return Promise.reject(new Error(message));
  }
);

// 封装GET请求
export function get<T>(url: string, params?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.get(url, { params, ...config });
}

// 封装POST请求
export function post<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.post(url, data, config);
}

// 封装PUT请求
export function put<T>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.put(url, data, config);
}

// 封装DELETE请求
export function del<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
  return service.delete(url, config);
}

export default service;