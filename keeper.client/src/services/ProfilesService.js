import { AppState } from "../AppState";
import { api } from "./AxiosService";

class ProfilesService {

  async GetActiveProfile(userId) {
    const res = await api.get(`api/profiles/${userId}`);
    console.log('[GetActiveProfile]', res.data);
    AppState.profile = res.data
  }

}

export const profilesService = new ProfilesService();