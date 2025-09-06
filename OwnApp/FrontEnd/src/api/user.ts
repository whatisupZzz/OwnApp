import { LoginParams, LoginResult, UserInfo } from '@/types/user';
import { mockLogin, mockGetUserInfo } from '@/mock';

// 是否使用模拟数据
const useMock = true;

// 登录接口
export function login(params: LoginParams): Promise<any> {
  if (useMock) {
    return mockLogin(params);
  }
  // 实际环境中使用真实API
  // return post<LoginResult>('/user/login', params);
  throw new Error('真实API尚未实现');
}

// 获取用户信息
export function getUserInfo(): Promise<any> {
  if (useMock) {
    const token = localStorage.getItem('token') || '';
    return mockGetUserInfo(token);
  }
  // 实际环境中使用真实API
  // return get<UserInfo>('/user/info');
  throw new Error('真实API尚未实现');
}

// 登出接口
export function logout(): Promise<any> {
  return Promise.resolve({
    code: 200,
    message: '登出成功',
    data: null
  });
}