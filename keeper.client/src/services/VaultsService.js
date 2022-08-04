import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";


class VaultsService {

  async getMyVaults() {
    const res = await api.get(`account/vaults`)
    logger.log('[GetMyVaults]', res.data)
    AppState.MyVaults = res.data;
  }

  async GetActiveUserVaults(userId) {
    const res = await api.get(`api/profiles/${userId}/vaults`)
    logger.log('[GetActiveUserVaults]', res.data)
    AppState.ActiveVaults = res.data
  }

  async GetActiveVault(id) {
    const res = await api.get(`api/vaults/${id}`);
    console.log('[GetActiveVault]', res.data);
    AppState.ActiveVault = res.data
  }

  async GetCurrentVaultKeeps(id) {
    const res = await api.get(`api/vaults/${id}/keeps`);
    console.log('[GetCurrentVaultKeeps]', res.data);
    AppState.CurrentVaultKeeps = res.data
  }

  async DeleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`);
    console.log('[DeleteVault]', res.data);
    AppState.MyVaults = AppState.MyVaults.filter(mv => mv.id != vaultId)
  }

}

export const vaultsService = new VaultsService();