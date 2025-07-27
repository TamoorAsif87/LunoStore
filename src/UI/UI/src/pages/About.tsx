import { useTitle } from "../hooks/useTitle";

function About() {
  useTitle("About | LunoStore");
  return (
    <>
      <section className="max-w-[1100px] mx-auto px-4 py-12">
        <h1 className="text-4xl font-bold text-primary mb-4 tracking-tight">
          About <span className="text-accent">LunoStore</span>
        </h1>

        <p className="text-lg text-gray-700 mb-8 leading-relaxed">
          At <strong className="text-primary">LunoStore</strong>, we're
          passionate about delivering premium products that reflect elegance,
          quality, and care. Our mission is to provide a seamless and satisfying
          online shopping experience for all your lifestyle and fashion needs.
        </p>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          <div className="bg-base-100 p-6 rounded-xl shadow-md">
            <h2 className="text-xl font-semibold text-secondary mb-2">
              Our Story
            </h2>
            <p className="text-gray-600">
              Founded with a vision to redefine online retail, LunoStore has
              grown into a trusted name that values creativity, customer
              service, and modern design. We’re not just a store—we’re a
              community.
            </p>
          </div>

          <div className="bg-base-100 p-6 rounded-xl shadow-md">
            <h2 className="text-xl font-semibold text-secondary mb-2">
              What We Offer
            </h2>
            <p className="text-gray-600">
              From fashion-forward apparel to home essentials, we offer
              carefully curated collections that blend luxury with
              affordability. Our platform is easy to navigate, secure, and
              tailored to your shopping habits.
            </p>
          </div>
        </div>

        <div className="mt-12 text-center">
          <p className="text-gray-500 text-sm">
            © {new Date().getFullYear()} LunoStore — All rights reserved.
          </p>
        </div>
      </section>
    </>
  );
}

export default About;
