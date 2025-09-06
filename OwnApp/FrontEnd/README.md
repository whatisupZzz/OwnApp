# OwnApp 前端项目

## 项目介绍

这是一个基于Vue3、TypeScript、Vite、Element Plus的前端项目，实现了用户登录、首页等基本功能。

## 技术栈

- Vue3
- TypeScript
- Vite
- Vue Router
- Pinia
- Element Plus
- Axios
- SCSS

## 项目结构

```
├── public              # 静态资源
├── src                 # 源代码
│   ├── api             # API接口
│   ├── assets          # 静态资源
│   ├── components      # 公共组件
│   ├── mock            # 模拟数据
│   ├── plugins         # 插件
│   ├── router          # 路由
│   ├── store           # 状态管理
│   ├── styles          # 全局样式
│   ├── types           # 类型定义
│   ├── utils           # 工具函数
│   ├── views           # 页面
│   ├── App.vue         # 入口组件
│   └── main.ts         # 入口文件
├── .env.development    # 开发环境配置
├── .env.production     # 生产环境配置
├── index.html          # HTML模板
├── package.json        # 项目依赖
├── tsconfig.json       # TypeScript配置
├── vite.config.ts      # Vite配置
└── README.md           # 项目说明
```

## 开发环境

```bash
# 安装依赖
npm install

# 启动开发服务器
npm run dev
```

## 构建

```bash
# 构建生产环境
npm run build
```

## 功能列表

- [x] 用户登录
- [x] 首页
- [x] 路由管理
- [x] 状态管理
- [ ] 权限控制
- [ ] 主题切换
- [ ] 国际化
