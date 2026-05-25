import { Orbitron, Exo } from "next/font/google";
import type { AppProps } from "next/app";
import "@/styles/globals.css";

const orbitron = Orbitron({
  variable: "--font-Orbitron",
  weight: ["400", "500", "600", "700"],
  subsets: ["latin"],
});

const exo = Exo({
  variable: "--font-Exo",
  weight: ["300", "400", "500", "600", "700"],
  subsets: ["latin"],
});

export default function App({ Component, pageProps }: AppProps) {
  return (
    <main className={`${exo.variable} ${orbitron.variable} font-sans`}>
      <Component {...pageProps} />
    </main>
  );
}