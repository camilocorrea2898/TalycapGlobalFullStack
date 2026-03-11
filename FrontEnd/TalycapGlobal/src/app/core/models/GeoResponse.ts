export interface GeoResponse {
  name: string;
  local_names: { [key: string]: string }; // diccionario con traducciones en distintos idiomas
  lat: number;
  lon: number;
  country: string;
  state?: string; // opcional, porque no siempre viene
}
