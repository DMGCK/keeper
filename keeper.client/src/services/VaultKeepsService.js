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

}

export const vaultKeepsService = new VaultKeepsService();