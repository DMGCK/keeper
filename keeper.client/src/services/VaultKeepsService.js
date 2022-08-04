import { AppState } from "../AppState";
import { api } from "./AxiosService";


class VaultKeepsService {

  async GetMyVaultKeeps() {
    const res = await api.get(`account/vaultkeeps`)
    AppState.MyVaultKeeps = res.data
  }

  async AddToVault(vKeepData) {
    const res = await api.post('api/vaultkeeps', vKeepData)
    console.log('[AddToVault]', res.data);
    AppState.MyVaultKeeps = [res.data, ...AppState.MyVaultKeeps];
  }

  async deleteVKeep(vKeepId, keepId) {
    const res = await api.delete(`api/vaultkeeps/${vKeepId}`);
    console.log('[deleteVKeep', res.data);
    AppState.CurrentVaultKeeps = AppState.CurrentVaultKeeps.filter(k => k.id != keepId)
  }

}

export const vaultKeepsService = new VaultKeepsService();