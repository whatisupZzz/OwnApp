// 模拟API服务
import { LoginParams, LoginResult } from '@/types/user';

// 模拟用户数据
const users = [
  {
    id: 1,
    username: 'admin',
    password: '123456',
    nickname: '管理员',
    avatar: 'https://randomuser.me/api/portraits/men/1.jpg',
    email: 'admin@example.com',
    roles: ['admin'],
    permissions: ['*']
  },
  {
    id: 2,
    username: 'user',
    password: '123456',
    nickname: '普通用户',
    avatar: 'https://randomuser.me/api/portraits/women/1.jpg',
    email: 'user@example.com',
    roles: ['user'],
    permissions: ['read']
  }
];

// 模拟登录API
export function mockLogin(params: LoginParams): Promise<LoginResult> {
  return new Promise((resolve, reject) => {
    // 模拟网络延迟
    setTimeout(() => {
      const user = users.find(u => u.username === params.username && u.password === params.password);
      
      if (user) {
        // 登录成功
        const { password, ...userInfo } = user;
        resolve({
          code: 200,
          message: '登录成功',
          data: {
            token: `mock-token-${user.id}-${Date.now()}`,
            userInfo
          }
        });
      } else {
        // 登录失败
        reject({
          code: 401,
          message: '用户名或密码错误',
          data: null
        });
      }
    }, 1000);
  });
}

// 模拟获取用户信息API
export function mockGetUserInfo(token: string): Promise<any> {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      if (token) {
        // 从token中提取用户ID
        const userId = parseInt(token.split('-')[1]);
        const user = users.find(u => u.id === userId);
        
        if (user) {
          const { password, ...userInfo } = user;
          resolve({
            code: 200,
            message: '获取用户信息成功',
            data: userInfo
          });
        } else {
          reject({
            code: 401,
            message: '无效的token',
            data: null
          });
        }
      } else {
        reject({
          code: 401,
          message: 'token不能为空',
          data: null
        });
      }
    }, 500);
  });
}