import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { vaultKeepsService } from "./VaultKeepsService"
import { vaultsService } from "./VaultsService"
import { keepsService } from "./KeepsService"

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('Something went Wrong', err)
    }

    try {
      await vaultsService.getMyVaults();
      await vaultKeepsService.GetMyVaultKeeps();
      await keepsService.GetActiveUserKeeps(AppState.account.id);
    } catch (err) {
      logger.error('Account Requests Failed', err)
    }
  }
}

export const accountService = new AccountService()
