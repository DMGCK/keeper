import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  profile: {},
  keeps: [],
  ActiveKeep: {},

  MyVaults: [],
  ActiveVaults: [],
  ActiveVault: {},

  MyVaultKeeps: [],
  ActiveVaultKeeps: [],

  MyKeeps: [],
  ActiveUserKeeps: [],
  CurrentVaultKeeps: []
  // this one is for the vault page no touch
})
