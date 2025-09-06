// 登录参数接口
export interface LoginParams {
  username: string;
  password: string;
  remember?: boolean;
}

// 登录结果接口
export interface LoginResult {
  code: number;
  message: string;
  data: {
    token: string;
    userInfo: UserInfo;
  } | null;
}

// 用户信息接口
export interface UserInfo {
  id: number;
  username: string;
  nickname?: string;
  avatar?: string;
  email?: string;
  roles: string[];
  permissions?: string[];
}