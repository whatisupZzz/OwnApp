import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { setupStore } from './store'
import './style.css'
import './styles/index.scss'

// 引入Element Plus
import ElementPlus from './plugins/element'

// 引入NProgress进度条
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

// 配置NProgress
NProgress.configure({ 
  easing: 'ease',
  speed: 500,
  showSpinner: false
})

// 路由守卫
router.beforeEach((to, from, next) => {
  NProgress.start()
  next()
})

router.afterEach(() => {
  NProgress.done()
})

const app = createApp(App)

// 配置路由
app.use(router)

// 配置状态管理
setupStore(app)

// 配置Element Plus
app.use(ElementPlus)

app.mount('#app')
