# Instala dependencias del sistema
package { ['apt-transport-https', 'ca-certificates', 'curl', 'gnupg', 'lsb-release']:
  ensure => installed,
}

# Agrega la llave GPG de Docker
exec { 'add-docker-gpg':
  command => 'curl -fsSL https://download.docker.com/linux/ubuntu/gpg | gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg',
  path    => ['/usr/bin', '/bin'],
  creates => '/usr/share/keyrings/docker-archive-keyring.gpg',
  require => Package['curl'],
}

# Agrega el repositorio de Docker
exec { 'add-docker-repo':
  command => 'echo "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu jammy stable" > /etc/apt/sources.list.d/docker.list',
  path    => ['/bin', '/usr/bin'],
  creates => '/etc/apt/sources.list.d/docker.list',
  require => Exec['add-docker-gpg'],
}

# Actualiza apt e instala Docker
exec { 'apt-update':
  command => 'apt-get update',
  path    => ['/usr/bin', '/bin'],
  require => Exec['add-docker-repo'],
}

package { ['docker-ce', 'docker-ce-cli', 'containerd.io', 'docker-compose-plugin']:
  ensure  => installed,
  require => Exec['apt-update'],
}

# Habilita y arranca Docker
service { 'docker':
  ensure  => running,
  enable  => true,
  require => Package['docker-ce'],
}